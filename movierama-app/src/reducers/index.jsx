import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { movies } from './movies.reducer';
import { alert } from './alert.reducer';
import { loader } from './loader.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  movies,
	alert,
	loader
});

export default rootReducer;