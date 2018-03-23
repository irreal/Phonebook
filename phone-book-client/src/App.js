import React, { Component } from 'react';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    // let phoneNumber = { number: "+381 60 412 84 22", "numberType": 0 }
    // let phoneNumber2 = { number: "+381 69 212 84 22", "numberType": 1 }
    // let contact = { firstName: "Miloš", lastName: "Spasojević", phoneNumbers: [phoneNumber, phoneNumber2] }
    this.state = {contacts: []};
  }
  
  componentDidMount() {
    var _this = this;
    // this.serverRequest = axios.get("http://codepen.io/jobs.json").then(function(result) {    
    //       _this.setState({
    //         contacts: result.data.jobs
    //       });
    //     })
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <h1 className="App-title">Welcome to the PhoneBook application!</h1>
        </header>
        <div className="App-intro">
          {this.state.contacts.map(c=> { return <ContactComponent contact={c} />})}
          
        </div>
      </div>
    );
  }
}

class PhoneNumberComponent extends Component {
  render() {
    return (
      <div>{this.props.phoneNumber.number}</div>
    );
  }
}
class ContactComponent extends Component {
  render() {
    return (
      <div>
        <div>{this.props.contact.firstName} {this.props.contact.lastName}</div>
        <div>{this.props.contact.phoneNumbers.map(pn => {
          return <PhoneNumberComponent phoneNumber={pn} />
        })}</div>
      </div>
    );
  }
}

export default App;
