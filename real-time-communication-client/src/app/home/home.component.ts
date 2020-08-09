import { Component, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { Message } from '../models/message.model';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {

  //npm install @microsoft/signalr

  user: string="";
  messages: any[]=[];
  message: string="";
  connection : signalR.HubConnection;

  constructor(private _messageService: MessageService) { }

  ngOnInit() {
     this.connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44327/messagehub")
    .build();


    this.connection.on("messageReceived", (message: Message) => {
      this.receiveMessageFromHub(message);
    });
  
    this.connection.start().catch(err => console.error(err));
  }

  sendMessage(){
    let newMessage: Message = {
      user : this.user,
      body : this.message
    }

    this.connection.send("newMessage", newMessage)
        .then(() => this.message = "");
  }

  receiveMessageFromHub(newMessage: any){
    this.messages.push(newMessage);
  }

  sendViaApi(){
    let newMessage: Message = {
      user : this.user,
      body : this.message
    }
    this._messageService.create(newMessage)
      .subscribe(ok => this.message = "");
  }


}
