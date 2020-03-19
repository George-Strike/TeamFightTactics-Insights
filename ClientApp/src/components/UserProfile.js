import React, { Component } from 'react';
import SearchBar from './SearchBar';
import PlayerData from './PlayerData';
import './home.css';
import authService from './api-authorization/AuthorizeService';


export class UserProfile extends Component {

    constructor(props) {
        super(props);
        this.state = { player: {}, hidePlayerData: true, SignedInName: "" };

        this.renderSearchBar = this.renderSearchBar.bind(this);
        this.renderPlayerData = this.renderPlayerData.bind(this);
        this.currentUserInfo = this.currentUserInfo.bind(this);
    }
    rank = "";
    renderSearchBar() {
        return (
            <SearchBar onResult={function (data) {
                this.setState({ player: data, hidePlayerData: false });
                if (this.props.location.pathname !== `/profile/${this.state.player.region}/${this.state.player.username}`) {
                    this.props.history.push(`/profile/${this.state.player.region}/${this.state.player.username}`, { player: data, hidePlayerData: false });
                }
                console.log(this.state.hidePlayerData);
            }.bind(this)}
            />
        );
    }

    renderPlayerData() {
        return (
            <PlayerData data={this.state.player} hideState={this.state.hidePlayerData} />
        );
    }
    async currentUserInfo() {
        const user = await authService.getUser();
        return user;
    }

    async componentDidMount() {
        let userDetails = await this.currentUserInfo();
        if (userDetails != null) {

            this.setState({ loadSpinner: true, SignedInName: userDetails.name != null ? userDetails.name : "" }, () => fetch('api/Tft/CheckPlayerData', {
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    signedInUserName: this.state.SignedInName
                })
            }).then(function (response) { return response.json(); })
                .then(function (data) {
                    console.log(data);
                    if (this.props.location.pathname !== `/profile/${this.state.player.region}/${this.state.player.username}`) {
                        return this.props.history.push(`/profile/${this.state.player.region}/${this.state.player.username}`, { loadSpinner: false, player: data, hidePlayerData: false });
                    }
                }.bind(this)))
        }
    }

    displayName = UserProfile.name
    render() {
        let searchBar = this.renderSearchBar();
        let signedInUserName = this.state.SignedInName;
        let player = this.renderPlayerData();
        if (Object.keys(this.state.player).length === 0) {
            return (
                <div className="title-image">
                    <h1 className="main-title">Welcome To TeamFight Tactics Insights, {signedInUserName}!</h1>
                    <p className="p-below-header">To begin with, please enter your username and correct region. These will be saved as your core profile.</p>
                    {searchBar}
                </div>
            );
        }
        else {
            this.setState({ hidePlayerData: false });
            return (
                <div>
                    {player}
                </div>
            );
        }
    }
}
