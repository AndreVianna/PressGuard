import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { LoggerComponent } from './components/logger/logger.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DeviceComponent } from './components/device/device.component';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        LoggerComponent,
        DashboardComponent,
        DeviceComponent,
    ],
    imports: [
        BrowserModule,
        HttpClientModule
    ],
    providers: [
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
