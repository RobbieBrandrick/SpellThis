///<reference path="../../typings/index.d.ts"/>
import {NgModule} from "@angular/core";  
import {BrowserModule} from "@angular/platform-browser";  
import {HttpModule} from "@angular/http";  
import "rxjs/Rx";

import { WordListComponent } from "./word-list.component";

@NgModule({
    // directives, components, and pipes
    declarations: [
        WordListComponent
    ],
    // modules
    imports: [
        BrowserModule,
        HttpModule
    ],
    // providers
    providers: [
    ],
    bootstrap: [
        WordListComponent
    ]
})
export class AppModule { }  