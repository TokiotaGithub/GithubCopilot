import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BeersService } from '../beers.service';
import { Beer } from '../beer';

@Component({
  selector: 'app-beer',
  templateUrl: './beer.component.html',
  styleUrls: ['./beer.component.css'],
  standalone: true,
  providers: [BeersService]
})
export class BeerComponent implements OnInit {
  beer!: Beer;   

  constructor(
    private route: ActivatedRoute,
    private beersService: BeersService
  ) { }

  ngOnInit() {
    this.getBeer();
  }

  getBeer(): void {
    const id:number = +Number(this.route.snapshot.paramMap.get('id'));    
    this.beersService.getBeer(id).subscribe(beer => this.beer = beer);

  }

  updateBeer(): void {
    this.beersService.updateBeer(this.beer).subscribe();
  }
}
