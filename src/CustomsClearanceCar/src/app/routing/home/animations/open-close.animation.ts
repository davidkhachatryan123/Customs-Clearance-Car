import { animate, style, transition, trigger, state } from "@angular/animations";

export const OpenCloseAnimation =
trigger('slideOpenClose', [
  state('open', style({
    height: '*',
  })),
  state('close', style({
    opacity: '0',
    height: '0px'
  })),
  transition('close => open', animate('200ms ease-in-out')),
  transition('open => close', animate('200ms ease-in-out'))
]);