import React, { Component } from 'react';
import './App.css';
import { ApplicationBar } from './components/ApplicationBar';

import { HomePage } from './views/HomePage';
import { MoviePage } from './views/MoviePage';
import { LoginPage } from './views/LoginPage';
import { RegisterPage } from './views/RegisterPage';

import { BrowserRouter, Route } from 'react-router-dom';
import { history } from './helpers';
import { alertActions } from './actions';
import { connect } from 'react-redux';

class App extends Component {

	constructor(props) {
		super(props);

		const { dispatch } = this.props;
		history.listen((location, action) => {
			// clear alert on location change
			dispatch(alertActions.clear());
		});
	}

	render() {
		const { alert } = this.props;
		return (
			<BrowserRouter>
				<div>
					<ApplicationBar />
					{alert.message &&
						<div className={`alert ${alert.type}`}>{alert.message}</div>
					}
					<Route exact path="/" component={HomePage} />
					<Route path="/movie" component={MoviePage} />
					<Route path="/login" component={LoginPage} />
					<Route path="/register" component={RegisterPage} />
				</div>
			</BrowserRouter>
		);
	}
}

function mapStateToProps(state) {
	const { alert } = state;
	return {
		alert
	};
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 
