import { MyhighlightDirective } from './myhighlight.directive';
import { ElementRef } from '@angular/core';

describe('MyhighlightDirective', () => {
  it('should create an instance', () => {
    var el: ElementRef;
    const directive = new MyhighlightDirective(el);
    expect(directive).toBeTruthy();
  });
});
