import api from "../../../api"
import * as mutations from "./mutation_types"
import { GET_PROFILE } from "./action_types"

export default {
    state: {
        profile_pending: false,
        themes: [],
        subjects: [],
        group: {
            id: null,
            name: ''
        }
    },
    mutations: {
        [mutations.PROFILE_REQUEST]: state => {
            state.profile_pending = true;
        },
        [mutations.PROFILE_SUCCESS]: (state, data) => {
            state.profile_pending = false;
            state.themes = data.themes;
            state.subjects = data.subjects;
            state.group = data.group;
        },
        [mutations.PROFILE_FAILURE]: (state) => {
            state.profile_pending = false;
        }
    },
    actions: {
        [GET_PROFILE]: ({ commit }) => {
            commit(mutations.PROFILE_REQUEST);

            api.get('profile').then(response => {
                commit(mutations.PROFILE_SUCCESS, response.data);
            }).catch(error => {
                commit(mutations.PROFILE_FAILURE);
                console.log(error);
            });
        }
    },
    getters: {
        themes: state => {
            return state.themes;
        },
        subjects: state => {
            return state.subjects;
        },
        group: state => {
            return state.group;
        }
    }
}