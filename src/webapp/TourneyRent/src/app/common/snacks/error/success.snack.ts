import { Component } from '@angular/core';

@Component({
  template: ` <span class="success">Success</span> `,
  styles: [
    `
      .success {
        color: lightgreen;
      }
    `,
  ],
})
export class SuccessSnackComponent {
}
