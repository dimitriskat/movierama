import React from 'react';
import { connect } from 'react-redux';

import { movieActions } from '../actions';

import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import FormControl from '@material-ui/core/FormControl';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
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
	form: {
		marginTop: theme.spacing.unit,
	},
	submit: {
		marginTop: theme.spacing.unit * 3,
	},
});

class MoviePage extends React.Component {
	constructor(props) {
		super(props);

		this.state = {
			movie: {
				title: '',
				description: ''
			},
			submitted: false
		};

		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleChange(event) {
		const { name, value } = event.target;
		const { movie } = this.state;
		this.setState({
			movie: {
				...movie,
				[name]: value
			}
		});
	}

	handleSubmit(event) {
		event.preventDefault();

		this.setState({ submitted: true });
		const { movie } = this.state;
		const { dispatch } = this.props;
		if (movie.title && movie.description) {
			var redirect = () => this.props.history.push('/');
			dispatch(movieActions.post(movie, redirect));
		}
	}
	
	render() {
		const { classes } = this.props;
		const { movie } = this.state;

		return <React.Fragment>
			<CssBaseline />
			<main className={classes.layout}>
				<Paper className={classes.paper}>
					<Typography variant="headline">New Movie</Typography>
					<form name="form" className={classes.form} onSubmit={this.handleSubmit}>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="title">Title</InputLabel>
							<Input
								id="title"
								name="title"
								autoFocus
								value={movie.title}
								onChange={this.handleChange} />
						</FormControl>
						<FormControl margin="normal" required fullWidth>
							<InputLabel htmlFor="description">Description</InputLabel>
							<Input
								id="description"
								name="description"
								value={movie.description}
								onChange={this.handleChange} />
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
	}
}

function mapStateToProps(state) {
	const { registering } = state.registration;
	return {
		registering
	};
}

const connectedMoviePage = withStyles(styles)(connect(mapStateToProps)(MoviePage));
export { connectedMoviePage as MoviePage };