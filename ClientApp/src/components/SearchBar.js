
import React, { Component } from 'react';
import { withRouter } from 'react-router';
import './searchbar.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import authService from './api-authorization/AuthorizeService';

class SearchBar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: '',
            loadSpinner: false,
            region: "euw1",
            signedInUserName: ""
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    handleChange = (e) => {
        const target = e.target;
        const value = target.value;
        const name = target.name;
        this.setState({
            region: target.type === 'select-one' && this.state.region ? value : this.state.region ? this.state.region : value,
            [name]: value
        });
    }

    handleSubmit = async (e) => {
        e.preventDefault();
        let signedInUser = await authService.getUser();
        const { usernameSent } = this.state;
        const username = usernameSent;
        let region = this.state.region;
        this.setState({ loadSpinner: true, signedInUserName: signedInUser != null ? signedInUser.name : "" }, () => fetch('api/Tft/RetrieveData', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                region: region,
                username: username,
                signedInUserName: this.state.signedInUserName
            })
        }).then(function (response) { return response.json(); })
            .then(function (data) {
                console.log(data);
                this.setState({ loadSpinner: false });
                return this.props.onResult(data);
            }.bind(this)));
    }

    render() {
        let loadingSpinner = "";
        if (this.state.loadSpinner) {
            loadingSpinner =
                <div>
                    <div className="loading-spinner">
                        <div className="inner one" />
                        <div className="inner two" />
                        <div className="inner three" />
                    </div>
                    <p className="loading-text"><em>Waiting for player data...</em></p>
                </div>;
        }
        return (
            <div className="search-container">
                <form className="search-form" onSubmit={this.handleSubmit}>
                    <select id="region" onChange={this.handleChange}>
                        <option value="euw1" onSelect={this.state.region === "euw1"}>EU West</option>
                        <option value="na1" onSelect={this.state.region === "na1"}>North America</option>
                    </select>
                  
                    <input
                        className="search-box"
                        type="text"
                        placeholder="Type Username"
                        name="usernameSent"
                        ref="search-player"
                        //value={this.state.value}
                        onChange={this.handleChange}
                    />
                    <button className="search-button" type="submit">
                        <span className="search-icon">
                            <FontAwesomeIcon icon={faSearch} />
                        </span>
                    </button>
                </form>
                {loadingSpinner}
            </div>
        );
    }
}
export default withRouter(SearchBar);