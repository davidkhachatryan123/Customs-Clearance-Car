export class Car {
  public constructor(
    public mark: string,
    public model: string | null,
    public engineCapacity: number | null,
    public year: number
  ) { }
}