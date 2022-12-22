import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GenderService } from './gender.service';
import { HttpClient } from '@angular/common/http';
import { Gender } from '../models/api-models/gender.model';

let baseurl = 'https://localhost:7154/';

describe('GenderService', () => {
  let genderService: GenderService;
  let http: HttpClient;
  let httpController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GenderService]
    });
    genderService = TestBed.inject(GenderService);
    http = TestBed.inject(HttpClient);
    httpController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(genderService).toBeDefined();
  });



  it('gender list', () => {

    const dummyData: Gender[] = [
        {id: '54182038-4ABF-42FF-B05A-0F4C414CBC8B', description: 'Female'},
        {id: '177A07F2-3493-49A4-A720-AC96C51C7C43', description: 'Other'},

    ]
    // const testData = true;
    genderService.getGenderList().subscribe(result => {
      expect(result).toEqual(dummyData);
    });
    const req = httpController.expectOne(baseurl + 'gender');
    expect(req.request.method).toBe('GET');
    req.flush(dummyData);
  });

});
