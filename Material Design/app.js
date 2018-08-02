
import {MDCRipple} from '@material/ripple';
const ripple = new MDCRipple(document.querySelector('.foo-button'));

import {MDCTextField} from '@material/textfield';
const textField = new MDCTextField(document.querySelector('.mdc-text-field'))

import {MDCSnackbar, MDCSnackbarFoundation} from '@material/snackbar';
const snackbar = new MDCSnackbar(document.querySelector('.mdc-snackbar'));


const dataObj = {
  message: 'Loaded',
  actionText: 'Undo',
  actionHandler: function () {
    console.log('my cool function');
  }
};

snackbar.show(dataObj);