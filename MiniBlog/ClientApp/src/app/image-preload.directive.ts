import { Directive, Input, HostBinding, Renderer2, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: 'img[default]',
})
export class ImagePreloadDirective {
  @Input() default: string;

  constructor(private elementRef: ElementRef) { }

  @HostListener("error")
  loadFallback() {
    const element: HTMLImageElement = <HTMLImageElement>this.elementRef.nativeElement;
    element.src = this.default;
  }
}
