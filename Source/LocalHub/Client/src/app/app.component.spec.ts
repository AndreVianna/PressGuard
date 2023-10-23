import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { LoggerComponent } from 'src/app/components/logger/logger.component';
import { HeaderComponent } from 'src/app/components/header/header.component';
import { DashboardComponent } from 'src/app/components/dashboard/dashboard.component';
import { DeviceComponent } from 'src/app/components/device/device.component';

describe('AppComponent', () => {
    let component: AppComponent;
    let fixture: ComponentFixture<AppComponent>;
    let httpMock: HttpTestingController;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [
                AppComponent,
                HeaderComponent,
                LoggerComponent,
                DashboardComponent,
                DeviceComponent,
            ],
            imports: [HttpClientTestingModule]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppComponent);
        component = fixture.componentInstance;
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should create the app', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize without erros', () => {
        component.ngOnInit();
    });
});
