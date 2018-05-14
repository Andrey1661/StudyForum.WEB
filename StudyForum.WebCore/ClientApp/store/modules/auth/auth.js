import api from "../../../api"
import * as mutations from "./mutation_types"
import * as actions from "./action_types"

export default {
    state: {
        login_pending: false,
        group_request_pending: false,
        user: null,
        groups: []
    },
    mutations: {
        [mutations.AUTH_REQUEST]: state => {
            state.login_pending = true;
        },
        [mutations.AUTH_SUCCESS]: (state, data) => {
            state.login_pending = false;
            state.user = data;
        },
        [mutations.AUTH_FAILURE]: state => {
            state.login_pending = false;
        },
        [mutations.AUTH_LOGOUT]: state => {
            state.isLoggedIn = false;
            state.user = null;
        },
        [mutations.GROUP_LIST_REQUEST]: state => {
            state.group_request_pending = true;
        },
        [mutations.GROUP_LIST_SUCCESS]: (state, data) => {
            state.group_request_pending = false;
            state.groups = data;
        },
        [mutations.GROUP_LIST_FAILURE]: state => {
            state.group_request_pending = false;
        }
    },
    actions: {
        [actions.LOGIN]: (context, data) => {
            context.commit(mutations.AUTH_REQUEST);
            api.post('login', data).then(response => {

                context.commit(mutations.AUTH_SUCCESS, response.data.user);

                var token = "Bearer " + response.data.token;
                api.defaults.headers.common["Authorization"] = token;

                localStorage.setItem('token', token);
                localStorage.setItem('userData', response.data.user);

            }).catch(error => {
                context.commit(mutations.AUTH_FAILURE);
                console.log(error);
            });
        },
        [actions.LOGOUT]: context => {
            context.commit(mutations.AUTH_LOGOUT);
            localStorage.removeItem('token');
            localStorage.removeItem('userData');
            api.default.headers.common["Authorization"] = '';
        },
        [actions.GET_GROUP_LIST]: ({ commit }, search) => {
            commit(mutations.GROUP_LIST_REQUEST);

            api.get('groups?query=' + search).then(response => {
                commit(mutations.GROUP_LIST_SUCCESS, response.data);
            });
        }
    },
    getters: {
        isLoggedIn: state => {
            return state.user !== null;
        },
        getGroups: state => {
            return state.groups;
        }
    }
}