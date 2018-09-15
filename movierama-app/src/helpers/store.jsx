import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import { createLogger } from 'redux-logger';
import rootReducer from '../reducers';

var activeMidleware = [thunkMiddleware];

if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
	activeMidleware.push(createLogger())
}

export const store = createStore(
    rootReducer,
    applyMiddleware(...activeMidleware)
);