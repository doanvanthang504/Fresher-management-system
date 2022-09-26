import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ActivatedNavItemContext from "./contexts/ActivatedNavItemContext";
import Empty from "./components/Empty";
import Constants from "./Constants";

import Sidebar from "./components/Sidebar";
import HomePage from "./routes/homepage";
import Header from "./components/Header";
import Footer from "./components/Footer";
import FresherReport from "./routes/fresherReport";
import FeedbackPage from "./routes/feedback";
import Reminder from "./routes/reminder";

import ClassFresher from "./pages/ClassFresher";
import ImportClassFresher from "./pages/ClassFresher/ImportClassFresherPage";
import CreateClassFresher from "./pages/ClassFresher/CreateClassFresherPage";
import DetailClassFresher from "./pages/ClassFresher/DetailClassFresher";
import EditClassFresher from "./pages/ClassFresher/EditClassFresherr";
import { AddNewFeedback } from "./components/feedback/addNewFeedback";
import { FeedbackDetails } from "./components/feedback/feedbackDetail";
import { UpdateFeedback } from "./components/feedback/updateFeedback";
import Login from "./routes/login";
import Score from "./components/Scores/Score";
import ScoreDetail from "./components/Scores/ScoreDetail";

class App extends React.Component {
  state = {
    activatedItem: 0,
    setActivatedItem: (item) => {
      this.setState({
        activatedItem: item,
        headerText: Constants.Routes[item].shortText,
      });
    },
  };

  render() {
    return (
      <ActivatedNavItemContext.Provider value={this.state}>
        <Router>
          <Header />

          <Sidebar activatedItem={this.state.activatedItem} />

          <div className="content-wrapper">
            <div className="content mt-3">
              <div className="container-fuild">
                <Switch>
                  <Route path="/" exact component={HomePage} />
                  <Route path="/login" component={Login} />
                  <Route path="/attendances" component={Empty} />
                  <Route path="/scores/:id/:module?" component={ScoreDetail} />
                  <Route path="/scores" component={Score} />
                  <Route path="/classes" exact component={ClassFresher} />
                  <Route
                    path="/classes/detail/:id"
                    component={DetailClassFresher}
                  />
                  <Route
                    path="/classes/edit/:id"
                    component={EditClassFresher}
                  />
                  <Route
                    path="/classes/import"
                    component={ImportClassFresher}
                  />
                  <Route
                    path="/classes/create"
                    component={CreateClassFresher}
                  />                  
                  <Route path="/fresher-report" component={FresherReport} />
                  <Route path="/feedback/create" component={AddNewFeedback} />
                  <Route
                    path="/feedback/update/:id/"
                    component={UpdateFeedback}
                  />
                  <Route path="/feedback/:id/" component={FeedbackDetails} />
                  <Route path="/feedback" component={FeedbackPage} />
                  <Route path="/reminders" component={Reminder} />                  
                          
                </Switch>
              </div>
            </div>
          </div>
        </Router>
        <Footer />
      </ActivatedNavItemContext.Provider>
    );
  }
}

export default App;
