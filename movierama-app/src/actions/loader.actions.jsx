import { loaderConstants } from '../constants';

export const loaderActions = {
	show,
	hide
};

function show() {
	return { type: loaderConstants.SHOW };
}

function hide() {
	return { type: loaderConstants.HIDE };
}
