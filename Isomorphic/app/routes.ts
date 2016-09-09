import { RouterConfig } from '@angular/router';
import { AppComponent } from './main';

export const routes: RouterConfig = [
    { path: '', redirectTo: 'home' },
    { path: 'home', component: AppComponent },
    { path: '**', redirectTo: 'home' }
];