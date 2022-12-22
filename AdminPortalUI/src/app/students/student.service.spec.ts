import { TestBed } from '@angular/core/testing';
import { async } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { StudentService } from './student.service';
import { HttpClient } from '@angular/common/http';
import { Student } from '../models/api-models/student.model';
import { Gender } from '../models/api-models/gender.model';
import { Address } from '../models/api-models/address.model';

let baseurl = 'https://localhost:7154/';

describe('StudentService', () => {
  let studentService: StudentService;
  let http: HttpClient;
  let httpController: HttpTestingController;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StudentService]
    });
    studentService = TestBed.inject(StudentService);
    http = TestBed.inject(HttpClient);
    httpController = TestBed.inject(HttpTestingController);
  }));

  const testData: Student[] = [
    {id: '89087B68-3C4D-4CF4-97F4-14AD6FA955BD', name: 'Jamaal Albert', dateOfBirth: '1989-06-14 00:00:00.0000000', email: 'Jamaal.Albert@gmaill.com', profileImg: '', genderId: '54182038-4ABF-42FF-B05A-0F4C414CBC8B',
    gender: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
    address: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', physicalAddress:'', postalAddress:''}
  }
  ];



 /* it('should be created', () => {
    expect(studentService).toBeDefined();
  }); */

  it('student list', () => {
    studentService.getAllStudents().subscribe(res => {
      expect(res).toEqual(testData);
    });

    const req = httpController.expectOne(baseurl + 'student');
    expect(req.request.method).toEqual('GET');
    req.flush(testData);
  });

  it('student id', () => {
    const studentId = '89087B68-3C4D-4CF4-97F4-14AD6FA955BD';
    const testData = {
      id: '89087B68-3C4D-4CF4-97F4-14AD6FA955BD', name: 'Jamaal Albert', dateOfBirth: '1989-06-14 00:00:00.0000000', email: 'Jamaal.Albert@gmaill.com', profileImg: '', genderId: '54182038-4ABF-42FF-B05A-0F4C414CBC8B',
      gender: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
      address: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', physicalAddress:'', postalAddress:''}
    }
    studentService.getstudent(studentId).subscribe(res => {
      expect(res).toEqual(testData);
    });

    const req = httpController.expectOne(baseurl + 'student/' + studentId);
    expect(req.request.method).toEqual('GET');
    req.flush(testData);
  });


  it('create student', () => {

    const testData = {
      id: '89087B68-3C4D-4CF4-97F4-14AD6FA955BD', name: 'Jamaal Albert', dateOfBirth: '1989-06-14 00:00:00.0000000', email: 'Jamaal.Albert@gmaill.com', profileImg: '', genderId: '54182038-4ABF-42FF-B05A-0F4C414CBC8B',
      gender: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
      address: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', physicalAddress:'', postalAddress:''}
    }

    // const studId = '63afdfe6-a692-456b-83f3-cc090d2878e0';
    studentService.addStudent(testData).subscribe(res => {
      expect(res).toEqual(testData);
    });

    const req = httpController.expectOne(baseurl + 'student/add');
    expect(req.request.method).toEqual('POST');
    req.flush(testData);
  });


  it('delete student', () => {
    const studentId = '89087B68-3C4D-4CF4-97F4-14AD6FA955BD';
    const testData = {
      id: '89087B68-3C4D-4CF4-97F4-14AD6FA955BD', name: 'Jamaal Albert', dateOfBirth: '1989-06-14 00:00:00.0000000', email: 'Jamaal.Albert@gmaill.com', profileImg: '', genderId: '54182038-4ABF-42FF-B05A-0F4C414CBC8B',
      gender: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
      address: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', physicalAddress:'', postalAddress:''}
    }
    studentService.deleteStudent(studentId).subscribe(res => {
      expect(res).toEqual(testData);
    });

    const req = httpController.expectOne(baseurl + 'student/' + studentId);
    expect(req.request.method).toEqual('DELETE');
    req.flush(testData);
  });


  it('update student', () => {
    const studentId = '89087B68-3C4D-4CF4-97F4-14AD6FA955BD';
    const testData = {
      id: '89087B68-3C4D-4CF4-97F4-14AD6FA955BD', name: 'Jamaal Albert', dateOfBirth: '1989-06-14 00:00:00.0000000', email: 'Jamaal.Albert@gmaill.com', profileImg: '', genderId: '54182038-4ABF-42FF-B05A-0F4C414CBC8B',
      gender: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
      address: {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', physicalAddress:'', postalAddress:''}
    }
    studentService.updateStudent(studentId, testData).subscribe(res => {
      expect(res).toEqual(testData);
    });

    const req = httpController.expectOne(baseurl + 'student/' + studentId);
    expect(req.request.method).toEqual('PUT');
    req.flush(testData);

  });

});

