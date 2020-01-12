import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import Header from './Header';
import {BackHandler} from 'react-native';
import {
  HeaderButtons,
  HeaderButton,
  Item,
} from 'react-navigation-header-buttons';
import React from 'react';
import axios from 'axios';
import CommonDataManager from './CommonDataManager';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  TextInput,
  StatusBar,
  Button,
  Alert,
} from 'react-native';

import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
class Login extends React.Component {
  state = {musteriNo: '', TCKN: '', sifre: '', status: ''};
  static navigationOptions = {
    drawerLabel: () => null,
  };
  loginOl() {
    if (this.state.TCKN.length === 11) {
      if (this.state.sifre.length === 6) {
        axios({
          method: 'post',
          url:
            'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/user/login',
          data: {
            TCKN: this.state.TCKN,
            sifre: this.state.sifre,
          },
        }).then(response => {
          console.log(response.status);
          this.setState({status: response.status});
          if (this.state.status == '200') {
            this.setState({musteriNo: response.data.musteriNo});
            console.log(this.state.musteriNo);
            let commonData = CommonDataManager.getInstance();
            commonData.setMusteriNo(this.state.musteriNo);
            this.props.navigation.navigate('Anasayfa', {
              musteriNo: this.state.musteriNo,
            });
          } else {
            this.setState({musteriNo: response.data.musteriNo});
            Alert.alert(response.data);
          }
        });
      } else {
        Alert.alert('Şifreniz 6 haneli olmalıdır!');
      }
    } else {
      Alert.alert('TCKN 11 haneli olmalıdır!');
    }
  }
  render() {
    const {ViewStyle, subViewstyle, TextStyle, buttonStyle} = styles;
    return (
      <>
        <SafeAreaView>
          <Header headerText="Giriş Ekranı" />
          <View style={ViewStyle}>
            <View style={subViewstyle}>
              <TextInput
                placeholder="TC Kimlik No"
                style={TextStyle}
                value={this.state.TCKN}
                onChangeText={text => this.setState({TCKN: text})}
                maxLength={11}
                keyboardType={'numeric'}
              />
            </View>
            <View style={subViewstyle}>
              <TextInput
                placeholder="Şifre"
                style={TextStyle}
                value={this.state.sifre}
                onChangeText={text => this.setState({sifre: text})}
                secureTextEntry={true}
                maxLength={6}
                keyboardType={'numeric'}
              />
            </View>
            <View style={buttonStyle}>
              <Button title="Login" onPress={() => this.loginOl()} />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Kayıt Ol"
                onPress={() => this.props.navigation.navigate('Details')}
              />
            </View>
          </View>
        </SafeAreaView>
      </>
    );
  }
}
const styles = {
  ViewStyle: {
    borderWidth: 1,
    borderRadius: 2,
    borderColor: '#ddd',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'center',
  },
  buttonStyle: {
    flexDirection: 'row',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'flex-end',
    position: 'relative',
  },
  subViewstyle: {
    borderWidth: 1,
    borderRadius: 20,
    borderColor: '#ddd',
    marginTop: 10,
    marginRight: 5,
    marginLeft: 5,
    padding: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextStyle: {
    color: 'black',
    fontSize: 20,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
  },
};
export default Login;
