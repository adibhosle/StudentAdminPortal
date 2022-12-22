import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddStudentRequest } from '../models/api-models/addstudentreq.model';
import { Student } from '../models/api-models/student.model';
import { UpdateStudentRequest } from '../models/api-models/updatestudentreq.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseurl = 'https://localhost:7154';

  constructor(private httpClient: HttpClient) { }

  getAllStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseurl + '/student');
  }

  getstudent(studentId: string): Observable<Student> {
    return this.httpClient.get<Student>(this.baseurl + '/student/' + studentId);
  }

  updateStudent(studentId: string, studentRequest: Student): Observable<Student> {

    const updateStudentRequest: UpdateStudentRequest = {
      name: studentRequest.name,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      genderId: studentRequest.genderId,
      physicalAddress: studentRequest.address.physicalAddress,
      postalAddress: studentRequest.address.postalAddress
    }

    return this.httpClient.put<Student>(this.baseurl + '/student/' + studentId, updateStudentRequest);
  }

  deleteStudent(studentId: string): Observable<Student> {
    return this.httpClient.delete<Student>(this.baseurl + '/student/' + studentId);
  }

  addStudent(studentRequest: Student): Observable<Student> {
    const addStudentRequest: AddStudentRequest = {
      name: studentRequest.name,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      genderId: studentRequest.genderId,
      physicalAddress: studentRequest.address.physicalAddress,
      postalAddress: studentRequest.address.postalAddress
    };

    return this.httpClient.post<Student>(this.baseurl + '/student/add', addStudentRequest);
  }


  uploadImage(studentId: string, file: File): Observable<any> {
    const formData = new FormData();
    formData.append("profileImage", file);

    return this.httpClient.post(this.baseurl + '/student/' + studentId + '/upload-image',
      formData, {
      responseType: 'text'
    }
    );
  }

  getImagePath(relativePath: string) {
    return `${this.baseurl}/${relativePath}`;
  }


}
