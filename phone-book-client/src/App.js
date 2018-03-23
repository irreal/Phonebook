import React, { Component } from 'react';
import './App.css';
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
      headers: { 'Access-Control-Allow-Origin': '*' },
      data: contact
    }).then(function (result) {
      this.setState({ newContactFirstName: "", newContactLastName: "", newContactPhoneNumber: "" });
      _this.loadAllContacts();
    }).catch(error => {
      alert('there was a problem creating the contact');
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
                    <ContactComponent contact={c} />
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

class PhoneNumbersComponent extends Component {
  render() {
    return (
      this.props.numbers.map(n => {
        return (
          <td key={n.number}>{n.number}</td>
        );
      })
    );
  }
}

class ContactComponent extends Component {
  render() {
    var cells = [];
    cells.push(<td key={1}>{this.props.contact.firstName}</td>);
    cells.push(<td key={2}>{this.props.contact.lastName}</td>);
    if (this.props.contact.phoneNumbers !== undefined && this.props.contact.phoneNumbers.length > 0) {
      cells.push(<PhoneNumbersComponent key={3} numbers={this.props.contact.phoneNumbers} />);
    }

    return cells;
  }
}

export default App;
