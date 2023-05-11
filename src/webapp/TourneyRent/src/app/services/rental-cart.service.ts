import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface RentalCartItem {
  id: number,
  selectedDays: Date[],
  price: any,
  
}

@Injectable({
  providedIn: 'root'
})
export class RentalCartService {
  private readonly LOCAL_STORAGE_KEY = "rentals";

  public readonly itemsUpdated$: Subject<boolean>;

  constructor() {
    this.itemsUpdated$ = new Subject<boolean>();
  }

  public getItems(): RentalCartItem[] {
    return JSON.parse(localStorage.getItem(this.LOCAL_STORAGE_KEY) ?? "[]") as RentalCartItem[];
  }

  public addItem(item: RentalCartItem) {
    const items = this.getItems();
    items.push(item);
    localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(items));
    this.itemsUpdated$.next(true);
  }

  public removeItem(item: RentalCartItem) {
    const items = this.getItems();
    const updatedItems = items.filter(i => i.id != item.id);
    localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(updatedItems));
    this.itemsUpdated$.next(true);
  }

  public reset() {
    localStorage.setItem(this.LOCAL_STORAGE_KEY, "[]");
    this.itemsUpdated$.next(true);
  }
}