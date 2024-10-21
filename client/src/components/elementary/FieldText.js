import React, {Component} from 'react';
import {Form} from "react-bootstrap";

class FieldText extends Component {
    state = {
      value: "",
        useProp: false
    };

    //height = 34;

    constructor(props) {
        super(props);
        if(this.props.useProp)
            this.state.useProp = true;
        if(this.props.value != null)
            this.state.value = this.props.value;
        else
            this.state.value = "";

        this.textareaRef = React.createRef();
    }

    handleChange = (valueN) => {
        this.setState({
            value: valueN
        });
        this.props.setValue(valueN, this.props.id);
    };

    componentDidMount() {
        if (this.textareaRef.current) {
            this.textareaRef.current.style.height = this.textareaRef.current.scrollHeight + 'px';
        }
    }

    render() {
        return (
            <div>
                <Form.Group className={"mb-1 mt-1 w-100 d-flex"}>
                    <div className={"W-30 d-flex text-end"}>
                        <Form.Label className="ms-auto me-3 mt-2" style={{textTransform: "capitalize"}}>{this.props.name}</Form.Label>
                    </div>

                    <div className="W-100 me-0">
                        <Form.Control
                            value={this.state.useProp || this.props.disabled? this.props.value:this.state.value}
                            as={this.props.rows != null?'textarea':"input"}
                            rows={this.props.rows}
                            ref={this.textareaRef}
                            //style={{height : this.height + "px"}}
                            //onLoad={e => this.height = e.target.}
                            //onInput={e => e.target.style.height = (e.target.scrollHeight + "px")}
                            onFocus={e => e.target.style.height = (e.target.scrollHeight + "px")}
                            className={this.props.value == null? "": this.props.value.length > this.props.maxlen?"text-danger":""}
                            onChange={e => {this.handleChange(e.target.value);e.target.style.height = (e.target.scrollHeight + "px")} }
                            placeholder={this.props.placeholder}
                            disabled={this.props.disabled}
                        />

                    </div>
                    <div className="">
                        {/*this.props.value == null?
                            <span className="float-end">{"0/" + this.props.maxlen}</span>
                            :
                            this.props.value.length > this.props.maxlen?
                                <span className="float-end text-danger">Превышена максимальная длина {
                                    " " + this.props.value.length + "/" + this.props.maxlen}</span>
                                :
                                this.props.value.length === 0 && this.props.nullable === false?
                                    <span className="float-end text-danger">Поле не заполнено
                                        {" " + this.props.value.length + "/" + this.props.maxlen}</span>
                                    :
                                    this.props.value.length < this.props.minlen?
                                        <span className="float-end text-danger">Слишком коротко {
                                            " " + this.props.value.length + "/" + this.props.maxlen}</span>
                                        :
                                        <span className="float-end">{this.props.value.length + "/" + this.props.maxlen}</span>*/
                        }
                    </div>


                </Form.Group>
            </div>
        );
    }
}

export default FieldText;