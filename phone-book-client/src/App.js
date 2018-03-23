import React, { Component } from 'react';
import './App.css';
import ContactComponent from './Components/ContactComponent.js'
import axios from "axios";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { contacts: [], newContactFirstName: "", newContactLastName: "", newContactPhoneNumber: "" };
  }


  loadAllContacts() {
    var _this = this;
    this.serverRequest = axios.get("http://phonebookserver20180323113131.azurewebsites.net/api/contacts").then(function (result) {
      _this.setState({
        contacts: result.data
      });
    }).catch(error => {
      alert('there was a problem loading the contacts');
      console.log(error);
    });
  }

  handleNameChange(event) {
    this.setState({ newContactFirstName: event.target.value });
  }

  handleLastNameChange(event) {
    this.setState({ newContactLastName: event.target.value });
  }

  handlePhoneNumberChange(event) {
    this.setState({ newContactPhoneNumber: event.target.value });
  }

  addNewNumber() {
    var number = { numberType: 0, number: this.state.newContactPhoneNumber };
    var contact = { firstName: this.state.newContactFirstName, lastName: this.state.newContactLastName, phoneNumbers: [number] };
    var _this = this;
    axios({
      method: 'post',
      url: 'http://phonebookserver20180323113131.azurewebsites.net/api/contacts',
      data: contact
    }).then(function (result) {
      _this.setState({ newContactFirstName: "", newContactLastName: "", newContactPhoneNumber: "" });
      _this.loadAllContacts();
    }).catch(error => {
      alert('there was a problem creating the contact');
      console.log(error);
    });
  }

  deleteContact(contact) {
    var _this = this;
    axios({
      method: 'delete',
      url: 'http://phonebookserver20180323113131.azurewebsites.net/api/contacts/' + contact.id
    }).then(function (result) {
      _this.loadAllContacts();
    }).catch(error => {
      alert('there was a problem deleting the contact');
      console.log(error);
    });
    
  }

  componentDidMount() {
    this.loadAllContacts();
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <h1 className="App-title">Welcome to the PhoneBook application!</h1>
        </header>
        <div className="App-intro">
          <p>
            Your contacts:
          </p>
          <button onClick={() => { this.loadAllContacts() }}>Refresh</button>
          <table>
            <thead>
              <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>PhoneNumbers</th>
              </tr>
            </thead>

            <tbody>
              {this.state.contacts.map(c => {
                return (
                  <tr key={c.id}>
                    <ContactComponent contact={c} handleDelete={(contact)=>this.deleteContact(contact)} />
                  </tr>
                );
              })}
            </tbody>
          </table>
          <form>
            <label>
              First name:
            <input type="text" value={this.state.newContactFirstName} onChange={(e) => this.handleNameChange(e)} />
            </label>
            <br />
            <label>
              Last name:
            <input type="text" value={this.state.newContactLastName} onChange={(e) => this.handleLastNameChange(e)} />
            </label>
            <br />
            <label>
              Phone number:
            <input type="text" value={this.state.newContactPhoneNumber} onChange={(e) => this.handlePhoneNumberChange(e)} />
            </label>
            <br />

          </form>
          <button onClick={() => this.addNewNumber()}>Add new</button>
        </div>
      </div>
    );
  }
}



export default App;
