import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoggerComponent } from './logger.component';
import { Log } from 'src/models';

describe('LoggerComponent', () => {
    let component: LoggerComponent;
    let fixture: ComponentFixture<LoggerComponent>;
    let httpMock: HttpTestingController;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [LoggerComponent],
            imports: [HttpClientTestingModule]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(LoggerComponent);
        component = fixture.componentInstance;
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should create the logger', () => {
        expect(component).toBeTruthy();
    });

    it('should addLog without erros', () => {
        const log = new Log();
        log.Level = 2;
        log.Message = "This is a log.";
        component.addLog(log);
    });
});
