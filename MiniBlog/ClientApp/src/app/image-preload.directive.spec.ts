import { ImagePreloadDirective } from './image-preload.directive';
import { Renderer2, ElementRef } from '@angular/core';
import { TestBed } from '@angular/core/testing';

describe('ImagePreloadDirective', () => {
  it('should create an instance', () => {
    const directive = new ImagePreloadDirective(TestBed.get(ElementRef));
    expect(directive).toBeTruthy();
  });
});
