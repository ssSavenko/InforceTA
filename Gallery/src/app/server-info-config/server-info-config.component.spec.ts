import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerInfoConfigComponent } from './server-info-config.component';

describe('ServerInfoConfigComponent', () => {
  let component: ServerInfoConfigComponent;
  let fixture: ComponentFixture<ServerInfoConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ServerInfoConfigComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ServerInfoConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
