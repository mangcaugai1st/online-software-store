import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserService} from '../../../services/user.service';
import {AddressService} from '../../../services/address.service';
import {User} from '../../../models/user.model';
import {UpdateUserProfileDto} from '../../../models/updateUserProfileDto';
import {NgForOf, NgIf} from '@angular/common';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    ReactiveFormsModule
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  user: User | null = null;
  isLoading: boolean = false;
  errorMessage: string = '';
  isEditProfileModalOpen: boolean = false;

  wards: any[];
  districts: any[];
  provinces: any[];

  selectedProvince: string;
  selectedDistrict: string;

  profileForm: FormGroup;

  constructor(private http: HttpClient,
              private userService: UserService,
              private addressService: AddressService,
              private fb: FormBuilder, ) { }

  ngOnInit() {
    this.displayUserInfoProfile();

    this.getProvinces();
    this.getDistricts();
    this.getWards();

    this.profileForm = this.fb.group({
      fullName: [''],
      email: [''],
      phone: [''],

      streetNumber: [''],
      ward: [''],
      district: [''],
      province: [''],
    })
  }

  displayUserInfoProfile() {
    this.isLoading = true;

    this.userService.getUserById().subscribe({
      next: (userData: User) => {
        this.user = userData;

        this.isLoading = false;

        this.profileForm.patchValue({
          fullName: this.user.fullName,
          email: this.user.email,
          phone: this.user.phone,
          address: this.user.address,
        })
      },
      error: (error: any) => {
        this.errorMessage = 'Không thể tải thông tin của bạn';
        this.isLoading = false;
        console.error('Lỗi trong trang hồ sơ ', error);
      }
    })
  }

  showEditProfileModal() {
    this.isEditProfileModalOpen = true;
  }

  closeEditProfileModal() {
    this.isEditProfileModalOpen = false;
  }

  getProvinces() {
    this.addressService.getProvinces().subscribe((data) => {
      this.provinces = data;
    })
  }

  getDistricts() {
    this.addressService.getDistricts().subscribe((data) => {
      this.districts = data;
    })
  }

  getWards() {
    this.addressService.getWards().subscribe((data) => {
      this.wards = data;
    })
  }

  onProvinceChange() {
    const selectedProvince = this.provinces.find(province => {});

    if (selectedProvince) {
      this.districts = selectedProvince.districts;
      this.selectedDistrict = '';
    }
  }

  // Phương thức gộp các trường địa chỉ thành một chuỗi duy nhất
  getFullAddress(): string {
    const streetNumber = this.profileForm.get('streetNumber')?.value;
    const ward = this.profileForm.get('ward')?.value;
    const district = this.profileForm.get('district')?.value;
    const province = this.profileForm.get('province')?.value;

    let fullAddress = '';

    if (streetNumber) fullAddress += `${streetNumber}, `;
    if (ward) fullAddress += `${ward}, `;
    if (district) fullAddress += `${district}, `;
    if (province) fullAddress += `${province}`;

    return fullAddress;
  }

  onSave(): void {
    const address = this.getFullAddress();

    const userData: UpdateUserProfileDto = {
      fullName: this.profileForm.get('fullName')?.value,
      email: this.profileForm.get('email')?.value,
      phone: this.profileForm.get('phone')?.value,
      address: address,
    };

    this.userService.updateUserProfile(userData).subscribe({
      next: (response: any) => {
        console.log("Cập nhật thông tin thành công: ", response);
      },
      error: (error: any) => {
        console.log('Lỗi khi lưu dữ liệu: ', error);
        console.log('Dữ liệu gửi đi: ', userData)
      },
      complete: () => {
        console.log('Dữ liệu đã gửi lên server: ', userData);
      }
    });
  }
}
