import React, { Component } from 'react';
import { withRouter } from 'react-router';
import './playerdata.css';



class PlayerData extends Component {
    constructor(props) {
        super(props);
        this.state = { hidePlayerData: this.props.hideState };
    }
    renderPlayerData(player) {
        return (
            <div className="player-data">
                <p>UserName: {Object.keys(player).map(key => player[key])[0]}</p>

                <p>Platform: {Object.keys(player).map(key => player[key])[4]}</p>

                <p>Level: {Object.keys(player).map(key => player[key])[1]}</p>

                <p>Percentage Required To Level Up: {Object.keys(player).map(key => player[key])[2]}%</p>

                <p>Rank: {this.rank = Object.keys(player).map(key => player[key])[3], Object.keys(this.rank).map(key => this.rank[key])[1]}</p>

                <img src={this.rank = Object.keys(player).map(key => player[key])[3], Object.keys(this.rank).map(key => this.rank[key])[3]} />
            </div>
        );
    }


    render() {
        let getPlayerData = "";
        if (!this.props.location.state.hidePlayerData && this.props.location.state.player) {         
            getPlayerData = this.renderPlayerData(this.props.location.state.player);
        }
        return (
            <div>
                {getPlayerData ? getPlayerData : "lol"}
            </div>
        );
    }
}
export default withRouter(PlayerData);
