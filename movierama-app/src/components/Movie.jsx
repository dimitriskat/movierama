import React from 'react';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import withStyles from '@material-ui/core/styles/withStyles';

const styles = theme => ({
	userOpinion: {
		margin: theme.spacing.unit,
		float: 'right',
	},
	userLink: {
		cursor: 'pointer',
		color: 'blue'
	}
});

class Movie extends React.Component {

	YEAR_DURATION_MINUTES = 365 * 24 * 60

	MONTH_DURATION_MINUTES = 30 * 24 * 60

	DAYS_DURATION_MINUTES = 24 * 60

	HOURS_DURATION_MINUTES = 60

	getTimeLabel(creationTime) {
		var creationDate = new Date(creationTime);
		var utcNow = this.getUtcNow();
		var minutesDiff = (utcNow - creationDate) / (60 * 1000);

		//Not totally precise in case of a leap year occurrence
		if (minutesDiff > this.YEAR_DURATION_MINUTES) {
			return Math.floor(minutesDiff / this.YEAR_DURATION_MINUTES) + ' years ago';
		} else if (minutesDiff > this.MONTH_DURATION_MINUTES) { //This is not precise either, but still sufficient for this case
			return Math.floor(minutesDiff / this.MONTH_DURATION_MINUTES) + ' months ago';
		} else if (minutesDiff > this.DAYS_DURATION_MINUTES) {
			return Math.floor(minutesDiff / this.DAYS_DURATION_MINUTES) + ' days ago';
		} else if (minutesDiff > this.HOURS_DURATION_MINUTES) {
			return Math.floor(minutesDiff / this.HOURS_DURATION_MINUTES) + ' hours ago';
		} else if (minutesDiff > 0) {
			return Math.floor(minutesDiff) + ' minutes ago';
		} else {
			return 'some seconds ago';
		}
	}

	getUtcNow() {
		var now = new Date();
		return new Date(now.getUTCFullYear(),
			now.getUTCMonth(),
			now.getUTCDate(),
			now.getUTCHours(),
			now.getUTCMinutes(),
			now.getUTCSeconds());
	}

	getLikesLabel() {
		return this.props.movie.likes + ' ' + (this.props.movie.likes !== 1 ? 'Likes' : 'Like')
	}

	getHatesLabel() {
		return this.props.movie.hates + ' ' + (this.props.movie.hates !== 1 ? 'Hates' : 'Hate')
	}

	isUserLoggedIn() {
		return this.props.loggedIn;
	}

	isPostedByCurrentUser() {
		return (this.props.loggedIn && this.props.user.id === this.props.movie.user)
	}

	userLikesMovie() {
		return this.props.movie.userOpinion === 0;
	}

	userHatesMovie() {
		return this.props.movie.userOpinion === 1;
	}

	render() {
		const { classes } = this.props;

		const {
			id,
			title,
			description,
			user,
			userFirstName,
			userLastName,
			hates,
			likes,
			creationTime } = this.props.movie;

		var isUserLoggedIn = this.isUserLoggedIn();
		var isPostedByCurrentUser = this.isPostedByCurrentUser();

		var postedByLabel = isPostedByCurrentUser ?
			<span>Posted by <a className={classes.userLink} onClick={() => this.props.onShowUserMovies(user, userFirstName + ' ' + userLastName)}>You</a> {this.getTimeLabel(creationTime)}</span> :
			<span>Posted by <a className={classes.userLink} onClick={() => this.props.onShowUserMovies(user, userFirstName + ' ' + userLastName)}>{userFirstName} {userLastName}</a> {this.getTimeLabel(creationTime)}</span>;

		var userLikesMovie = this.userLikesMovie();
		var userHatesMovie = this.userHatesMovie();
		var hasVote = hates || likes;

		var userOpinionLabel = userLikesMovie ? <span className={classes.userOpinion}>You like this movie
			<Button aria-label="Unlike" onClick={() => this.props.onMovieOpinionRevoke(id)}>Unlike</Button>
		</span> : userHatesMovie ? <span>You hate this movie
			<Button aria-label="Unhate" onClick={() => this.props.onMovieOpinionRevoke(id)}>Unhate</Button>
		</span> : '';

		return (
			<Card className='movie'>
				<CardHeader title={title} subheader={postedByLabel} />
				<CardContent>
					<Typography component="p">{description}</Typography>
				</CardContent>
				{hasVote ?
					<CardActions className='actions' disableActionSpacing >
						<span style={{ flex: 1 }} >
							<Button
								aria-label="Like"
								disabled={!isUserLoggedIn || isPostedByCurrentUser || userLikesMovie}
								onClick={() => this.props.onMovieLike(id)} >
								{this.getLikesLabel()}
							</Button>
							<Button
								aria-label="Hate"
								disabled={!isUserLoggedIn || isPostedByCurrentUser || userHatesMovie}
								onClick={() => this.props.onMovieHate(id)} >
								{this.getHatesLabel()}
							</Button>
						</span>
						{userOpinionLabel}
					</CardActions> : ((isUserLoggedIn && !isPostedByCurrentUser) ?
						<CardActions className='actions' disableActionSpacing >
							<span>Be the first to vote for this movie: </span>
							<Button
								aria-label="Like"
								onClick={() => this.props.onMovieLike(id)} >
								Like
								</Button>
							<Button
								aria-label="Hate"
								onClick={() => this.props.onMovieHate(id)} >
								Hate
						</Button>
						</CardActions> : '')}
			</Card>
		);
	}
}

const styledMovie = withStyles(styles)(Movie);
export { styledMovie as Movie };
