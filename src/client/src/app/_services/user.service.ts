import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IUser } from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.baseUrl + 'fragUser/users');
  }

  getUser(id: string): Observable<IUser> {
    return this.http.get<IUser>(this.baseUrl + 'fragUser/users/' + id);
  }

  updateUser(id: string, user: IUser) {
    return this.http.put(this.baseUrl + 'fragUser/users/' + id, user);
  }

  setMainPhoto(userId: string, id: string) {
    return this.http.post(this.baseUrl + 'photos/' + id + '/setMain/' + userId, {});
  }

}
