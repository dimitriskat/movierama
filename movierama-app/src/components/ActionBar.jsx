import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Chip from '@material-ui/core/Chip'
import Avatar from '@material-ui/core/Avatar'
import { Link } from 'react-router-dom'
import ArrowUpward from '@material-ui/icons/ArrowUpward';
import ArrowDownward from '@material-ui/icons/ArrowDownward';
import FaceIcon from '@material-ui/icons/Face';
import { sortingConstants } from '../constants';

const styles = theme => ({
	button: {
		margin: theme.spacing.unit,
	},
	container: {
		textAlign: 'center'
	}
});

class ActionBar extends React.Component {
	render() {
		const { classes, sorting, filters, loggedIn } = this.props;

		const sortByLike = sorting.field === 'likes',
			sortByHate = sorting.field === 'hates',
			sortByDate = sorting.field === 'date';

		const asc = sorting.order === sortingConstants.ASCENDING;

		return (
			<div className={classes.container}>
				{filters.user ?
					<Chip
						avatar={<Avatar><FaceIcon /></Avatar>}
						label={'Movies of ' + filters.user.name}
						onDelete={this.props.onRemoveUserFilter}
						className={classes.chip}
						color="primary"
					/> : ''}
				<Button
					variant="contained"
					color="default"
					className={classes.button}
					onClick={() => this.props.onSortFieldClick('likes', sortByLike && asc ? sortingConstants.DESCENDING : sortingConstants.ASCENDING)}>
					Likes
					{sortByLike ?
						(asc ? <ArrowUpward className={classes.rightIcon} /> :
							<ArrowDownward className={classes.rightIcon} />)
						: ''}
				</Button>
				<Button
					variant="contained"
					color="default"
					className={classes.button}
					onClick={() => this.props.onSortFieldClick('hates', sortByHate && asc ? sortingConstants.DESCENDING : sortingConstants.ASCENDING)}>
					Hates
					{sortByHate ?
						(asc ? <ArrowUpward className={classes.rightIcon} /> :
							<ArrowDownward className={classes.rightIcon} />)
						: ''}
				</Button>
				<Button
					variant="contained"
					color="default"
					className={classes.button}
					onClick={() => this.props.onSortFieldClick('date', sortByDate && asc ? sortingConstants.DESCENDING : sortingConstants.ASCENDING)}>
					Date
					{sortByDate ?
						(asc ? <ArrowUpward className={classes.rightIcon} /> :
							<ArrowDownward className={classes.rightIcon} />)
						: ''}
				</Button>
				<Button
					variant="contained"
					color="primary"
					className={classes.button}
					component={Link}
					to={loggedIn ? "/movie" : '/register'}>
					New Movie
      	</Button>
			</div>
		);
	}
}

ActionBar.propTypes = {
	classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(ActionBar);
