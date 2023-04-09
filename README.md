Full stack web application for tracking debts. Tech stack: C# - .NET API + JavaScript - Vue JS + Vuetify. The database choice of use is the default SQL Server Express local database provided by Visual studio, as this is a small project. The application makes use of the Identity framework for the authentication part and integrates JSON web tokens for authorization. Apart from the Login API endpoint, all endpoints are protected by JWT. On the front end, the routing happens is handled by the Vue Router. Route protection is applied for the home page of the app. Front end application states are tracked using Vuex state management. Overall design is taken care of by Vuetify (Google material design). Application features: CRUD (create-read-update-delete) on Accounts, which have nested CRUD Transactions, based on which the debt of the account is calculated.

# SQL Server Express
# C# .NET Core API
# JWT
# VUE 3
# Vue Router
# Vuex
# Vuetify
