import { Injectable, NgZone } from '@angular/core';
import { Subject } from 'rxjs';
import { Log } from 'src/models';

@Injectable({ providedIn: 'root' })
export class SignalGeneratorService {
    constructor(private ngZone: NgZone) { }

    public start(subject: Subject<MessageEvent<string>>, log: Subject<Log>): void {
        this.ngZone.runOutsideAngular(() => {
            setInterval(() => {
                this.ngZone.run(() => {
                    const timestamp = new Date().toISOString().slice(11, 23);
                    for (let j = 0; j < 12; j++) {
                        let simulatedData = `${9870+j},${timestamp}`;
                        for (let i = 0; i < 8; i++) {
                            const randomValue = Math.floor(Math.random() * 51);
                            simulatedData += `,${randomValue}`;
                        }
                        subject.next({ data: simulatedData } as MessageEvent<string>);
                    }
                });
            }, 500);
        });
    }
}
