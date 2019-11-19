import React, { Component } from 'react';
import spinner from './spinner.gif';
import { withRouter } from 'react-router';
import './searchbar.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

class SearchBar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: '',
            loadSpinner: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    handleChange = (e) => {
        const target = e.target;
        const value = target.type === 'radio' ? target.name : target.value;
        const name = target.name;

        this.setState({
            platform: target.type === 'radio' && this.state.platform ? value : this.state.platform ? this.state.platform : value,
            [name]: value
        });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        const { usernameSent } = this.state;
        const username = usernameSent;
        let platform = this.state.platform;

        this.setState({ loadSpinner: true }, () => fetch('api/Apex/RetrieveData', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                platform: platform,
                username: username
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
                    PS4
                    <input
                        name="PS4"
                        type="radio"
                        checked={this.state.platform === "PS4"}
                        onChange={this.handleChange}
                    />
                    X1
                    <input
                        name="X1"
                        type="radio"
                        checked={this.state.platform === "X1"}
                        onChange={this.handleChange}
                    />
                    PC
                    <input
                        name="PC"
                        type="radio"
                        checked={this.state.platform === "PC"}
                        onChange={this.handleChange}
                    />
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