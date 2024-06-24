import { Component, OnInit } from '@angular/core';
import { BeersService } from '../beers.service';
import { Beer } from '../beer';
import { NgFor,CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatGridListModule, } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-beer-list',
  templateUrl: './beer-list.component.html',
  styleUrls: ['./beer-list.component.css'],
  standalone: true,
  providers: [BeersService,ReactiveFormsModule],
  imports: [NgFor,RouterLink,CommonModule,MatGridListModule,MatButtonModule]
})
export class BeerListComponent implements OnInit {
  beers: Beer[] = [];
  constructor(private beersService: BeersService) { }

  ngOnInit() {
    this.getBeers();
    
    const beerForm = new FormGroup({
      name: new FormControl(''),
      type: new FormControl(''),
      alcoholContent: new FormControl(''),
    });
  }

  //metodo para crear una beer por defecto sin recibir parametros que podamos usar en el html
  createBeer(): Beer {
    const beer: Beer = {
      id: 26,
      nombre: 'Prueba',
      lema: 'Staut',
      alcoholPorVolumen: 15
    };
    return beer;
  }
  getBeers(): void {
    this.beersService.getBeers().subscribe(beers => this.beers = beers);
  }

  addBeer(beer: Beer): void {
    this.beersService.addBeer(beer).subscribe(beer => this.beers.push(beer));
  }

  updateBeer(beer: Beer): void {
    this.beersService.updateBeer(beer).subscribe();
  }

  deleteBeer(beer: Beer): void {
    this.beersService.deleteBeer(beer.id).subscribe();
    this.beers = this.beers.filter(b => b !== beer);
  }
}
