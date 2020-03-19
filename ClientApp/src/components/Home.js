import React, { Component } from 'react'; 
import SearchBar from './SearchBar';
import PlayerData from './PlayerData';
import './home.css';


export class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { player: {}, hidePlayerData: true };
        this.renderSearchBar = this.renderSearchBar.bind(this);
        this.renderPlayerData = this.renderPlayerData.bind(this);
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
    displayName = Home.name
    render() {
        let searchBar = this.renderSearchBar();  
        return (
            <div className="title-image">
                <h1 className="main-title">ENTER YOUR USERNAME <br />& SELECT YOUR PLATFORM</h1>
                <p className="p-below-header"> See you stats then brag or cry, depending on what you see ;)</p>
                {searchBar}
            </div>
        );
    }
}
