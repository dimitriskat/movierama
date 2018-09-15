import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@material-ui/core';
import { withRouter } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { userActions } from '../actions';

class ApplicationBar extends React.Component {

	handleLogout = (id) => this.props.dispatch(userActions.logout())

	render() {
		return (
			<AppBar position="static">
				<Toolbar>
					<Typography variant="title" color="inherit" className='flexGrow1'>
						<span onClick={() => this.handleOnClick('/')}>Movie Rama</span>
					</Typography>
					{!this.props.loggedIn ?
						<React.Fragment>
							<Button color="inherit" component={Link} to="/register">Register</Button>
							<Button color="inherit" component={Link} to="/login">Login</Button>
						</React.Fragment> :
						<React.Fragment>
							<span>Welcome back {this.props.user.firstName} {this.props.user.lastName}</span>
							<Button color="inherit" onClick={this.handleLogout}>Logout</Button>
						</React.Fragment>}
				</Toolbar>
			</AppBar>
		);
	}
}

function mapStateToProps(state) {
	const { authentication } = state;
	const { user, loggedIn } = authentication;
	return {
		loggedIn,
		user
	};
}

const connectedApplicationBar = connect(mapStateToProps)(withRouter(ApplicationBar));
export { connectedApplicationBar as ApplicationBar };
