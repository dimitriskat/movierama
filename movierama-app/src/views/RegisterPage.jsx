import React from 'react';
import { connect } from 'react-redux';

import { userActions } from '../actions';

import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import FormControl from '@material-ui/core/FormControl';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import LockIcon from '@material-ui/icons/LockOutlined';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import withStyles from '@material-ui/core/styles/withStyles';
import { Link } from 'react-router-dom';

const styles = theme => ({
	layout: {
		width: 'auto',
		marginLeft: theme.spacing.unit * 3,
		marginRight: theme.spacing.unit * 3,
		[theme.breakpoints.up(400 + theme.spacing.unit * 3 * 2)]: {
			width: 400,
			marginLeft: 'auto',
			marginRight: 'auto',
		},
	},
	paper: {
		marginTop: theme.spacing.unit * 8,
		display: 'flex',
		flexDirection: 'column',
		alignItems: 'center',
		padding: `${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px ${theme.spacing.unit * 3}px`,
	},
	avatar: {
		margin: theme.spacing.unit,
		backgroundColor: theme.palette.secondary.main,
	},
	form: {
		marginTop: theme.spacing.unit,
	},
	submit: {
		marginTop: theme.spacing.unit * 3,
	},
});

class RegisterPage extends React.Component {
	constructor(props) {
		super(props);

		this.state = {
			user: {
				firstName: '',
				lastName: '',
				username: '',
				password: ''
			},
			submitted: false
		};

		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleChange(event) {
		const { name, value } = event.target;
		const { user } = this.state;
		this.setState({
			user: {
				...user,
				[name]: value
			}
		});
	}

	handleSubmit(event) {
		event.preventDefault();

		this.setState({ submitted: true });
		const { user } = this.state;
		const { dispatch } = this.props;
		if (user.firstName && user.lastName && user.username && user.password) {
			var redirect = () => this.props.history.push('/login');
			dispatch(userActions.register(user, redirect));
		}
	}

	render() {
		const { classes } = this.props;
		const { registering } = this.props;
		const { user, submitted } = this.state;

		return <React.Fragment>
			<CssBaseline />
			<main className={classes.layout}>
				<Paper className={classes.paper}>
					<Avatar className={classes.avatar}>
						<LockIcon />
					</Avatar>
					<Typography variant="headline">Register</Typography>
					<form name="form" className={classes.form} onSubmit={this.handleSubmit}>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="firstName">First Name</InputLabel>
							<Input
								id="firstName"
								name="firstName"
								autoComplete="firstName"
								autoFocus
								value={user.firstName}
								onChange={this.handleChange} />
						</FormControl>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="lastName">Last Name</InputLabel>
							<Input
								id="lastName"
								name="lastName"
								autoComplete="lastName"
								value={user.lastName}
								onChange={this.handleChange} />
						</FormControl>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="username">Username</InputLabel>
							<Input
								id="username"
								name="username"
								autoComplete="username"
								value={user.username}
								onChange={this.handleChange} />
						</FormControl>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="password">Password</InputLabel>
							<Input
								id="password"
								name="password"
								type="password"
								autoComplete="current-password"
								value={user.password}
								onChange={this.handleChange}
							/>
						</FormControl>
						<Button
							type="submit"
							fullWidth
							variant="raised"
							color="primary"
							className={classes.submit} >
							Register
            </Button>
						<Button
							type="button"
							fullWidth
							variant="raised"
							color="secondary"
							className={classes.submit}
							component={Link}
							to="/">
							Cancel
            </Button>
					</form>
				</Paper>
			</main>
		</React.Fragment>

		// return (
		//     <div className="col-md-6 col-md-offset-3">
		//         <h2>Register</h2>
		//         <form name="form" onSubmit={this.handleSubmit}>
		//             <div className={'form-group' + (submitted && !user.firstName ? ' has-error' : '')}>
		//                 <label htmlFor="firstName">First Name</label>
		//                 <input type="text" className="form-control" name="firstName" value={user.firstName} onChange={this.handleChange} />
		//                 {submitted && !user.firstName &&
		//                     <div className="help-block">First Name is required</div>
		//                 }
		//             </div>
		//             <div className={'form-group' + (submitted && !user.lastName ? ' has-error' : '')}>
		//                 <label htmlFor="lastName">Last Name</label>
		//                 <input type="text" className="form-control" name="lastName" value={user.lastName} onChange={this.handleChange} />
		//                 {submitted && !user.lastName &&
		//                     <div className="help-block">Last Name is required</div>
		//                 }
		//             </div>
		//             <div className={'form-group' + (submitted && !user.username ? ' has-error' : '')}>
		//                 <label htmlFor="username">Username</label>
		//                 <input type="text" className="form-control" name="username" value={user.username} onChange={this.handleChange} />
		//                 {submitted && !user.username &&
		//                     <div className="help-block">Username is required</div>
		//                 }
		//             </div>
		//             <div className={'form-group' + (submitted && !user.password ? ' has-error' : '')}>
		//                 <label htmlFor="password">Password</label>
		//                 <input type="password" className="form-control" name="password" value={user.password} onChange={this.handleChange} />
		//                 {submitted && !user.password &&
		//                     <div className="help-block">Password is required</div>
		//                 }
		//             </div>
		//             <div className="form-group">
		//                 <button className="btn btn-primary">Register</button>
		//                 {registering && 
		//                     <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
		//                 }
		//                 <Link to="/login" className="btn btn-link">Cancel</Link>
		//             </div>
		//         </form>
		//     </div>
		// );
	}
}

function mapStateToProps(state) {
	const { registering } = state.registration;
	return {
		registering
	};
}

const connectedRegisterPage = withStyles(styles)(connect(mapStateToProps)(RegisterPage));
export { connectedRegisterPage as RegisterPage };