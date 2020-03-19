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
                <img src={"http://ddragon.leagueoflegends.com/cdn/10.5.1/img/profileicon/" + player.profileIconId + ".png"} alt="icon" />

                <p>Summoner Name: {player.username}</p>

                <p>Level: {player.level}</p>

                <p>Region: {player.region}</p>

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
