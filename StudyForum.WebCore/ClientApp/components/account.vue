<template>

    <div>
        <div class="account-box" v-bind:class="form.type">
            <div class="circle-top"></div>
            <img class="avatar" src="../assets/avatar2.png"/>
            <div>
                <div>
                    <span class="arrow arrow-left" v-on:click="switchType"></span>
                    <h3 style="display: inline-block">{{form.type === 'login' ? 'Вход в систему' : 'Регистрация'}}</h3>
                    <span class="arrow arrow-right" v-on:click="switchType"></span>
                </div>

                <div v-if="form.type === 'login'">
                    <form v-on:submit.prevent="onLoginSubmit">
                        <p>Email</p>
                        <input v-model="form.login.email.value" type="text" />
                        <p>Пароль</p>
                        <input v-model="form.login.password.value" type="password" />
                        <label class="checkbox-container">
                            <input v-model="form.login.rememberMe" type="checkbox" />
                            <span class="checkmark"></span>
                            <span class="checkbox-label">Запомнить</span>
                        </label>
                        <div>
                            <input type="submit" value="Отправить" />
                        </div>
                    </form>
                </div>
                <div v-else>
                    <form v-on:submit.prevent="onRegisterSubmit">
                        <p>Email</p>
                        <input v-model="form.register.email.value" type="text" />
                        <p>Пароль</p>
                        <input v-model="form.register.password.value" type="password" />
                        <p>Повтор пароля</p>
                        <input v-model="form.register.confirmPassword.value" type="password" />
                        <p>Учебная группа</p>
                        <v-select v-model="form.register.group" 
                                  :options="groups" 
                                  @search="loadGroups" 
                                  class="select"
                                  label="name">
                            <span slot="no-options">Начните вводить название группы...</span>
                        </v-select>
                        <div>
                            <input type="submit" value="Отправить" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import * as actions from '../store/modules/auth/action_types'

    export default {
        props: {
            inittype: {
                type: String,
                required: false,
                default: 'login',
                validator(value) {
                    return ['login', 'register'].indexOf(value) !== -1
                }
            }
        },
        data() {
            return {
                form: {
                    type: this.inittype,
                    login: {
                        email: { value: '', error: null },
                        password: { value: '', error: null },
                        rememberMe: false
                    },
                    register: {
                        email: { value: null, error: null },
                        password: { value: null, error: null },
                        confirmPassword: { value: null, error: null },
                        group: null
                    }
                }
            }
        },
        methods: {
            onLoginSubmit() {
                var body = {
                    email: this.form.login.email.value,
                    password: this.form.login.password.value,
                    rememberMe: this.form.login.rememberMe
                };
                this.$store.dispatch(actions.LOGIN, body);
                this.$router.push(this.$route.query.redirect || '/');
            },
            switchType() {
                this.form.type = this.form.type === 'login' ? 'register' : 'login';
            },
            loadGroups(search, loading) {
                if (search.length < 2) return;

                loading(true);
                this.$store.dispatch(actions.GET_GROUP_LIST, search);
                loading(false);
            }
        },
        computed: {
            groups() {
                return this.$store.getters.getGroups;
            }
        }
    }

</script>
<style src="../css/form.css"></style>
<style>
    .account-box{
        position: absolute;
        top: 50%;
        left: 50%;
        background: rgba(0, 0, 0, 0.7);
        transform: translate(-50%, -50%);
        padding: 50px 30px;
        color: white;
        text-align: center;
        border-radius: 5px;
        box-sizing: border-box;
    }

    .login {
        width: 360px;
        min-height: 420px;
    }

    .register{
        width: 360px;
        min-height: 480px;
    }

    .account-box h3, p {
        font-weight: bold;
    }

    .account-box p, label {
        margin: 0;
        margin-left: 10%;
        padding: 0;
        font-weight: bold;
        text-align: left;
    }

    .account-box input, .select {
        color: black;
        width: 80%;
        margin-bottom: 20px;
    }

    .account-box input[type="text"], input[type="password"], .select {
        border: none;
        border-bottom: 1px solid #fff;
        background: transparent;
        outline: none;
        height: 40px;
        color: #fff;
        font-size: 16px;
    }

    .select{
        margin-left: 10%;
    }

    .v-select .dropdown-toggle{
        border: none !important;
    }

    .v-select .dropdown-toggle input{
        color: white !important;
        padding: 0 !important;
    }

    .v-select ul.dropdown-menu{
        color: black !important;
    }

    span.selected-tag{
        color: white !important;
        padding: 0 !important;
    }

    .v-select .dropdown-toggle button.clear {
        color: lightgray !important;
        bottom: 6px !important;
    }

    .open-indicator:before{
        border-color: white !important;
    }

    .account-box input[type="submit"] {
        border: none;
        outline: none;
        height: 40px;
        background: #1c8adb;
        color: #fff;
        font-size: 18px;
        border-radius: 15px;
    }

    .account-box input[type="submit"]:hover {
        cursor: pointer;
        background: #1050db;
    }

    .circle-top {
        width: 100px;
        height: 50px;
        position: absolute;
        left: 50%;
        top: 0;
        transform: translate(-50%, -100%);
        border-top-left-radius: 100px;
        border-top-right-radius: 100px;
        background: rgba(0, 0, 0, 0.7);
    }

    .avatar {
        position: absolute;
        left: 50%;
        top: 0;
        transform: translate(-50%, -50%);
        width: 100px;
        height: 100px;
        border-radius: 50%;
    }

    .arrow {
        border: solid rgba(255, 255, 255, 1);
        display: inline-block;
        margin: 0 15px;
        padding: 8px;
        border-width: 0 5px 5px 0;
    }

    .arrow:hover {
        border: solid #1c8adb;
        border-width: 0 5px 5px 0;
        cursor: pointer;
    }

    .arrow-left {
        transform: rotate(135deg);
    }

    .arrow-right {
        transform: rotate(-45deg);
    }
</style>