import React from 'react'
import { Movie } from './Movie';
import { Grid } from '@material-ui/core';

class MovieList extends React.Component {
  render() {
    return <div style={{ padding: 20 }}>
      <Grid container spacing={24}>
        {(this.props.movies || []).map((item) => {
          return <Grid item xs={12} key={item.id}>
						<Movie 
							movie={item} 
							loggedIn={this.props.loggedIn} 
							user={this.props.user} 
							onMovieLike={this.props.onMovieLike} 
							onMovieHate={this.props.onMovieHate}
							onMovieOpinionRevoke={this.props.onMovieOpinionRevoke}
							onShowUserMovies={this.props.onShowUserMovies} />
          </Grid>
        })}
      </Grid>
    </div>
  }
}

export default MovieList