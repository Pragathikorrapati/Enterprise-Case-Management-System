import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
export interface CaseRecord { id: number; title: string; assignedTo: string; description: string; priority: 'Low'|'Medium'|'High'; status: 'Open'|'In Progress'|'Closed'; createdAtUtc: string; }
export interface CreateCase { title: string; assignedTo: string; description: string; priority: CaseRecord['priority']; }
@Injectable({providedIn:'root'})
export class CaseService {
  private http = inject(HttpClient);
  getCases(status: string, search: string): Observable<CaseRecord[]> {
    let params = new HttpParams().set('status', status).set('search', search);
    return this.http.get<CaseRecord[]>('/api/cases', {params});
  }
  createCase(payload: CreateCase): Observable<CaseRecord> { return this.http.post<CaseRecord>('/api/cases', payload); }
  updateStatus(id: number, status: CaseRecord['status']): Observable<CaseRecord> { return this.http.patch<CaseRecord>(`/api/cases/${id}/status`, {status}); }
}
