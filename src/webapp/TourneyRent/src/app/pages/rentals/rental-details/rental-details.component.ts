import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalResource } from 'src/app/resources/rental.resource';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-rental-details',
  templateUrl: './rental-details.component.html',
  styleUrls: ['./rental-details.component.scss']
})
export class RentalDetailsComponent implements OnInit {
  item: any;

  constructor(private dataService: RentalResource, private route: ActivatedRoute, public routing: RoutingService) { }

  ngOnInit(): void {
    const itemId = Number(this.route.snapshot.paramMap.get('id'));
    this.dataService.getItemById(itemId).subscribe((data: any) => {
      this.item = data;
    });
  }
}
