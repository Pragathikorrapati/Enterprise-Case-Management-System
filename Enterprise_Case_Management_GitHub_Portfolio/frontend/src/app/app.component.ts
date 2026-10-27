import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CaseRecord, CaseService, CreateCase } from './case.service';
@Component({selector:'app-root',standalone:true,imports:[CommonModule,FormsModule],templateUrl:'./app.component.html'})
export class AppComponent implements OnInit {
  private api = inject(CaseService);
  cases: CaseRecord[] = [];
  search = ''; filter = 'All'; loading = false; saving = false; showForm = false;
  feedback = ''; error = '';
  form: CreateCase = {title:'', assignedTo:'', description:'', priority:'Medium'};
  ngOnInit(): void { this.load(); }
  get openCount(): number { return this.cases.filter(c=>c.status==='Open').length; }
  get activeCount(): number { return this.cases.filter(c=>c.status==='In Progress').length; }
  get closedCount(): number { return this.cases.filter(c=>c.status==='Closed').length; }
  load(): void {
    this.loading=true; this.error='';
    this.api.getCases(this.filter,this.search).subscribe({
      next: data=>{this.cases=data;this.loading=false;},
      error: ()=>{this.error='Unable to load cases. Check that the API is running.';this.loading=false;}
    });
  }
  create(): void {
    if (!this.form.title.trim() || this.form.title.trim().length<3) {this.error='Title must be at least 3 characters.';return;}
    this.saving=true;this.error='';
    this.api.createCase(this.form).subscribe({
      next:()=>{this.saving=false;this.showForm=false;this.form={title:'',assignedTo:'',description:'',priority:'Medium'};this.feedback='Case created successfully.';this.load();},
      error:()=>{this.saving=false;this.error='Could not create case. Please try again.';}
    });
  }
  changeStatus(item: CaseRecord, status: CaseRecord['status']): void {
    this.error='';
    this.api.updateStatus(item.id,status).subscribe({
      next:()=>{this.feedback=`Status updated for case ${item.id}.`;this.load();},
      error:()=>this.error='Unable to update status.'
    });
  }
  trackById(_index:number,item:CaseRecord):number{return item.id;}
}
