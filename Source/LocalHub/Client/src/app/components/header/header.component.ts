import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval, Subscription } from 'rxjs';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
    public title = 'Local Hub';
    public currentDateTime: string = '';
    private dateTimeSubscription!: Subscription;

    ngOnInit(): void {
        this.dateTimeSubscription = interval(1000).subscribe(() => {
            this.currentDateTime = new Date().toLocaleString();
        });
    }

    ngOnDestroy(): void {
        this.dateTimeSubscription.unsubscribe();
    }
}
