import { AutoGroup } from "../models";

export const _filter = (opt: string[], value: string): string[] => {
  const filterValue = value.toLowerCase();

  return opt
  .filter(item => item !== '0')
  .filter(item => item.toLowerCase().includes(filterValue));
};

export const _filterGroup = (value: string, autoGroup: AutoGroup[]): AutoGroup[] => {
  if (value)
    return autoGroup
      .map(group => ({letter: group.letter, values: _filter(group.values, value)}))
      .filter(group => group.values.length > 0);

  return autoGroup;
};