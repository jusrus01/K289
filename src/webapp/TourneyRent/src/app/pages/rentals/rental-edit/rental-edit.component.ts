import { Component, OnInit } from '@angular/core';
import { RentalResource } from 'src/app/resources/rental.resource';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RoutingService } from 'src/app/services/routing.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-rental-edit',
  templateUrl: './rental-edit.component.html',
  styleUrls: ['./rental-edit.component.scss']
})
export class RentalEditComponent implements OnInit {
  itemId = Number(this.route.snapshot.paramMap.get('itemId'));
  //itemId!: number;
  item: any = {
    name: '',
    description: '',
    image: '',
    periodStart: '',
    periodEnd: '',
    price: 0
  };
  editForm: FormGroup;

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router, private itemService: RentalResource, public routing: RoutingService) { 
    this.editForm = new FormGroup({
      name: new FormControl(),
      description: new FormControl(),
      image: new FormControl(),
      periodStart: new FormControl(),
      periodEnd: new FormControl(),
      price: new FormControl()
    });
  }

  ngOnInit() {
    this.itemId = Number(this.route.snapshot.paramMap.get('itemId'));
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.itemService.getItemById(id).subscribe(item => {
      this.item = item;
      this.editForm.patchValue({
        name: item.name,
        description: item.description,
        image: item.image,
        periodStart: item.periodStart,
        periodEnd: item.periodEnd,
        price: item.price
      });
    });
  }

  submit(): void {
    this.itemService.updateItem(this.itemId, this.item).subscribe(() => {
      
    });
  }

}
