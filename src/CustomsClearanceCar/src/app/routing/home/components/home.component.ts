import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AutoGroup, Car } from '../models';
import { Observable, startWith, map, of } from 'rxjs';
import { _filter, _filterGroup } from '../consts';
import { OpenCloseAnimation } from '../animations/open-close.animation';
import { C3Service } from '../services/c3.service';
import { C3Extension } from '../extensions';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.css'],
  animations: [OpenCloseAnimation]
})
export class HomeComponent {

  //################################################
  //# Result variables
  //################################################

  @ViewChild("success", {static: false})
  successElement: ElementRef | undefined;
  
  @ViewChild("transparentLayer", {static: false})
  transparentLayerElement: ElementRef | undefined;

  result = 'Неизвестно';
  resultState = 'close';


  //################################################
  //# Form for car specifications
  //################################################

  carForm: FormGroup = new FormGroup({
    "mark": new FormControl({value: '', disabled: false}, [Validators.required]),
    "model": new FormControl({value: '', disabled: true}),
    "engine_capacity": new FormControl({value: '', disabled: true}, [Validators.pattern("(^[0-9]*$)|(Пустой)|(^$)")]),
    "year": new FormControl({value: '', disabled: true}, [Validators.required, Validators.pattern("^[0-9]{4}$")])
  });


  //################################################
  //# Input field autocomplete element values
  //################################################

  marksAutoGroup: AutoGroup[] = [];
  marksAutoGroupOptions: Observable<AutoGroup[]>;

  modelsAutoGroup: AutoGroup[] = [];
  modelsAutoGroupOptions: Observable<AutoGroup[]>;

  engineCapacitys: string[] = [];
  engineCapacitysOptions: Observable<string[]>;

  allowedYears: string[] = ["2023", "2022", "2021"]

  constructor(
    private c3Service: C3Service
  ) { }


  //################################################
  //# Handle input fields evnets for sending 
  //# requests for autocomplete data fetching
  //################################################

  onInputFocus($event) {
    switch($event.target.attributes.getNamedItem('formcontrolname').value) {
      case 'mark':
        this.c3Service.getMarks()
        .subscribe(marks => {
          this.marksAutoGroup = C3Extension.convertArrayToAutoGroup(marks);
          this.marksAutoGroupOptions = of(this.marksAutoGroup);

          this.marksAutoGroupOptions = this.carForm.get('mark')!.valueChanges.pipe(
            startWith(''),
            map(value => _filterGroup(value || '', this.marksAutoGroup)),
          );
        });
        break;
      case 'model':
        this.c3Service.getModels(
          this.carForm.controls['mark'].value)
        .subscribe(models => {
          this.modelsAutoGroup = C3Extension.convertArrayToAutoGroup(models);
          this.modelsAutoGroupOptions = of(this.modelsAutoGroup);

          this.modelsAutoGroupOptions = this.carForm.get('model')!.valueChanges.pipe(
            startWith(''),
            map(value => _filterGroup(value || '', this.modelsAutoGroup)),
          );
        });
        break;
      case 'engine_capacity':
        this.c3Service.getEngineCapacities(
          this.carForm.controls['mark'].value,
          this.carForm.controls['model'].value)
        .subscribe(engineCapacities => {
          this.engineCapacitys = engineCapacities.map(String);
          this.engineCapacitysOptions = of(this.engineCapacitys);

          this.engineCapacitysOptions = this.carForm.get('engine_capacity')!.valueChanges.pipe(
            startWith(''),
            map(value => _filter(this.engineCapacitys, value || '')),
          );
        });
        break;
    }
  }


  //################################################
  //# Submit form: get and show result
  //# Clear form: clear inputs and hide result box
  //################################################

  submit() {
    if (this.carForm.valid) {
      this.c3Service.calculate(new Car(
        this.carForm.get('mark').value.trim(),
        this.carForm.get('model').value.trim(),
        parseInt(this.carForm.get('engine_capacity').value.trim()),
        parseInt(this.carForm.get('year').value.trim())
      ))
      .subscribe(data =>
        this.result = data.result != null ? data.result : 'Неизвестно');

      this.resultState = 'open';
      if(this.successElement)
        this.successElement.nativeElement.src = 'assets/success.gif';
    }
  }

  clearForm() {
    this.carForm.reset();

    this.transparentLayerElement.nativeElement.style.opacity = '0';
    this.resultState = 'close';
  }


  //################################################
  //# Result animation for showing hidden layer
  //################################################

  resultAnimationDone($event) {
    if ($event.toState == 'open')
      this.transparentLayerElement.nativeElement.style.opacity = '1';
  }
}