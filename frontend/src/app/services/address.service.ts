import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  constructor(private httpClient: HttpClient) { }

  getProvinces() {
    return this.httpClient.get<any>('https://provinces.open-api.vn/api/p');
  }

  getDistricts() {
    return  this.httpClient.get<any>('https://provinces.open-api.vn/api/d');
  }

  getWards(){
    return this.httpClient.get<any>('https://provinces.open-api.vn/api/w');
  }
}
