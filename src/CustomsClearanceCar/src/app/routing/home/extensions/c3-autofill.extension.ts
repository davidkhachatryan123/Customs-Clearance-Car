import { AutoGroup } from "../models";

export class C3Extension {
  static convertArrayToAutoGroup(array: string[]): AutoGroup[] {
    const autoGroups: AutoGroup[] = [];
    let autoGroupTmp: AutoGroup = {letter: '', values: []};

    array.filter(el => el != null)
    .sort()
    .forEach(value => {
      if (autoGroupTmp.letter !== value[0] && autoGroupTmp.letter !== '') {
        autoGroups.push(autoGroupTmp);
        autoGroupTmp = {letter: '', values: []};
      }

      if (autoGroupTmp.letter === value[0] || autoGroupTmp.letter === '') {
        autoGroupTmp.letter = value[0];
        autoGroupTmp.values.push(value);
      }
    });

    autoGroups.push(autoGroupTmp);

    return autoGroups;
  }
}