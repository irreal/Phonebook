import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import ContactComponent from './Components/ContactComponent'

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
  ReactDOM.unmountComponentAtNode(div);
});

it('renders contacts', () => {
  let number = {numberType:0, number:"+381 60 412 84 22"};
  let contact = {firstName:'Miloš', lastName: 'Spasojević', phoneNumbers:[number]}
  const div = document.createElement('div');
  ReactDOM.render(<ContactComponent contact={contact} />, div);
  ReactDOM.unmountComponentAtNode(div);
});

