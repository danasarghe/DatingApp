import { MessageService } from './../../_services/message.service';
import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/members';
import { Message } from 'src/app/_models/message';
import { ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') mesageForm: NgForm;
  @Input() messages: Message[];
  @Input() username: string;
  messageContent: string;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }

  sendMessage(){
    this.messageService.sendMessage(this.username, this.messageContent).subscribe(message =>{
      this.messages.push(message);
      this.mesageForm.reset();
    })
  }


}
